using Blog.API.Application.Interfaces;

namespace Blog.API.MarkDig
{
	public class MarkdownConverter : IConverter<string, string>
	{
		public string Convert(string value)
		{
			return new Converter().ToHtml(value);
		}
	}
}