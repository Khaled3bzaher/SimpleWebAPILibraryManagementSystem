namespace SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResponse(bool success,int statusCode,T? data, string message = "")
        {
            return new ApiResponse<T> 
            { Success = success,StatusCode= statusCode,  Data = data, Message=message };
        }

        public static ApiResponse<T> FailureResponse(bool success, int statusCode,string message)
        {
            return new ApiResponse<T>
            { Success = success, StatusCode = statusCode, Message = message };
        }
    }
}
