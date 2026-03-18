namespace SampleApi.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int TotalCount { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Data retrieved successfully.")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            TotalCount = data is System.Collections.ICollection col ? col.Count : 1
        };
    }

    public static ApiResponse<T> NotFound(string message = "Record not found.")
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default,
            TotalCount = 0
        };
    }
}
