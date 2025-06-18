using CoWorkingProject.Server.DTOs;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CoWorkingProject.Server.Services
{
	public class GroqService
	{
		private readonly HttpClient httpClient;
		private readonly string apiKey;
		private readonly IBookingService bookingService;

		public GroqService(HttpClient httpClient, IOptions<GroqSettings> config, IBookingService bookingService)
		{
			this.httpClient = httpClient;
			this.httpClient.BaseAddress = new Uri("https://api.groq.com/openai/v1/");
			this.apiKey = config.Value.ApiKey;
			this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);
			this.bookingService = bookingService;
		}

		public async Task<string> AskAssistant(string question)
		{
			var userBookings = await this.bookingService.GetAllAsync();

			var prompt = this.BuildPrompt(question, userBookings);

			var request = new
			{
				model = "llama3-70b-8192",
				messages = new List<ChatMessage>
				{
					new ChatMessage { role = "system",
						content = @"You are a helpful assistant. You are given a structured list of bookings and must:
							- Count exactly how many bookings are provided.
							- Count exactly how many bookings are for the next weekend.
							- Use the WorkspaceType exactly as given (e.g. OpenSpace, MeetingRoom, PrivateRoom).
							- Do not guess. Only respond based on the data given.
							- Answear only what user wants from use, none other additional information.
							",
				},
					new ChatMessage { role = "user", content = prompt }
				},
				temperature = 0.2
			};

			var response = await this.httpClient.PostAsJsonAsync("chat/completions", request);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync();
				throw new Exception($"Groq API error: {error}");
			}

			using var stream = await response.Content.ReadAsStreamAsync();
			var json = await JsonDocument.ParseAsync(stream);
			var content = json.RootElement
				.GetProperty("choices")[0]
				.GetProperty("message")
				.GetProperty("content")
				.GetString();

			return content ?? "Sorry, I didn’t understand that.";
		}

		private string BuildPrompt(string question, IEnumerable<BookingDto> userBookings)
		{
			var sb = new StringBuilder();
			var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			sb.AppendLine($"Today is {currentDate}. ");
			sb.AppendLine("User bookings:");
			foreach (var b in userBookings.OrderBy(b => b.From))
			{
				sb.AppendLine($"- {b.WorkspaceType}, {b.Room.CapacityPerPerson} people, from {b.From:yyyy-MM-dd} to {b.To:yyyy-MM-dd}");
			}
			sb.AppendLine();
			sb.AppendLine($"Question: {question}");
			return sb.ToString();
		}
	}

	public class ChatRequest
	{
		public string model { get; set; } = "mixtral-8x7b-32768";
		public List<ChatMessage> messages { get; set; } = new();
		public double temperature { get; set; } = 0.2;
	}

	public class ChatMessage
	{
		public string role { get; set; } = string.Empty;
		public string content { get; set; } = string.Empty;
	}
}
