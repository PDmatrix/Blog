namespace Blog.API.Application.Interfaces
{
	public interface IConverter<in TInput, out TOutput>
	{
		TOutput Convert(TInput value);
	}
}