namespace AArkhipenko.Logging.Models
{
	/// <summary>
	/// Модель раздела логирования
	/// </summary>
	public class MethodLogModel
	{
		/// <summary>
		/// Название класса, из которого был выполнен вызов
		/// </summary>
		public string ClassName { get; set; } = string.Empty;

		/// <summary>
		/// Название метода, из которого был выполнен вызов
		/// </summary>
		public string MethodName { get; set; } = string.Empty;
	}
}
