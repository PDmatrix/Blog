using Markdig;

namespace Blog.API.MarkDig
{
	public class Converter
	{
		private readonly MarkdownPipeline _pipeline;
		
		public Converter()
		{
			_pipeline = new MarkdownPipelineBuilder()
				.UseSoftlineBreakAsHardlineBreak()
				.ConfigureNewLine(string.Empty)
				.UseAdvancedExtensions()
				.Build();
		}

		public string ToHtml(string content)
		{
			return Markdown.ToHtml(content, _pipeline);
		}
	}
}