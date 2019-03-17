using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Models;
using MediatR;

namespace Blog.API.Application.Posts.Queries
{
	public class PreviewQuery : IRequest<PreviewDto>
	{
		public string Content { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class PreviewHandler : RequestHandler<PreviewQuery, PreviewDto>
	{
		private readonly IConverter<string, string> _converter;
		
		public PreviewHandler(IConverter<string, string> converter)
		{
			_converter = converter;
		}
		
		protected override PreviewDto Handle(PreviewQuery request)
		{
			return new PreviewDto
			{
				Content = _converter.Convert(request.Content)
			};
		}
	}
}