using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoomermanServer.Patterns.ChainOfResponsibility
{
    public class ProfanityHandler : AbstractChatHandler
	{
		private static readonly string[] BadWords = new[]
				{
						"blogas",
						"blogis",
						"bl0gas",
						"bl0gis"
				};

		public override Message Handle(Message message)
		{
			if (ContainsBadWord(message.Text))
			{
				message.Text = Filter(message.Text, BadWords);
				return base.Handle(message);
			}

			return base.Handle(message);
		}

		public string Filter(string input, string[] badWords)
		{
			var re = new Regex(
					@"\b("
					+ string.Join("|", badWords.Select(word =>
							string.Join(@"\s*", word.ToCharArray())))
					+ @")\b", RegexOptions.IgnoreCase);
			return re.Replace(input, match =>
			{
				return new string('*', match.Length);
			});
		}

		public bool ContainsBadWord(string input)
		{
			return BadWords.Any(word => input.Contains(word, StringComparison.OrdinalIgnoreCase));
		}
	}
}
