using Blog.API.Application.Interfaces;
using MediatR;

namespace Blog.API.Application.Posts.Queries
{
	public class PreviewQuery : IRequest<string>
	{
		public string Content { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class PreviewHandler : RequestHandler<PreviewQuery, string>
	{
		private readonly IConverter<string, string> _converter;
		
		public PreviewHandler(IConverter<string, string> converter)
		{
			_converter = converter;
		}
		
		protected override string Handle(PreviewQuery request)
		{
			return _converter.Convert(request.Content);
		}
	}
}