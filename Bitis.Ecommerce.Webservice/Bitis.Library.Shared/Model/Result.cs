using System;
using System.Collections.Generic;
using System.Text;

namespace Bitis.Library.Shared.Model
{
    public interface IResult
    {
        bool Success { get; set; }
        int Code { get; set; }
        string Message { get; set; }
    }

    public interface IResult<T> : IResult
    {
        T Data { get; set; }
    }

    public interface IExceptionResult : IResult
    {
        IList<Exception> Errors { get; set; }
    }

    public interface IExceptionResult<T> : IResult<T>
    {
        IList<Exception> Errors { get; set; }
    }

    public interface IListResult<T> : IResult<IList<T>>
    {
        int TotalItems { get; set; }
    }

    public interface IPageResult<T> : IListResult<T>
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalPages { get; set; }
    }

    public class Result : IResult
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        public Result() { }

        public Result(bool success = true, int code = 0, string message = null)
        {
            Success = success;
            Code = code;
            Message = message;
        }

        public static IResult Ok(int code = 0, string message = null)
        {
            return new Result(success: true, code: code, message: message);
        }

        public static IResult Fail(int code = 0, string message = null)
        {
            return new Result(success: false, code: code, message: message);
        }

        public static IResult Exception(Exception exception, int code = 0, string message = null)
        {
            return new ExceptionResult(
                exception: exception,
                code: code,
                message: message);
        }

        public static IResult Exception(IList<Exception> exceptions, int code = 0, string message = null)
        {
            return new ExceptionResult(
                exceptions: exceptions,
                code: code,
                message: message);
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public Result() { }

        public Result(bool success = true, int code = 0, string message = null, T data = default(T))
            : base(success, code, message)
        {
            Data = data;
        }

        public static IResult Ok(int code = 0, string message = null, T data = default(T))
        {
            return new Result<T>(
                success: true,
                code: code,
                data: data,
                message: message);
        }

        public static IResult Fail(int code = 0, T data = default(T), string message = null)
        {
            return new Result<T>(
                success: false,
                code: code,
                data: data,
                message: message);
        }

        public static IResult Exception(Exception exception, int code = 0, T data = default(T), string message = null)
        {
            return new ExceptionResult<T>(
                exception: exception,
                data: data,
                code: code,
                message: message);
        }

        public static IResult Exception(IList<Exception> exceptions, int code = 0, T data = default(T), string message = null)
        {
            return new ExceptionResult<T>(
                exceptions: exceptions,
                data: data,
                code: code,
                message: message);
        }
    }

    public class ListResult<T> : Result<IList<T>>, IListResult<T>
    {
        public int TotalItems { get; set; }

        public ListResult() { }

        public ListResult(bool success = true, int code = 0, string message = null, IList<T> data = null)
            : base(success, code, message: message, data)
        {
            TotalItems = data?.Count ?? 0;
        }

        public new static IResult Ok(int code = 0, string message = null, IList<T> data = null)
        {
            return new ListResult<T>(
                success: true,
                code: code,
                data: data,
                message: message);
        }

        public new static IResult Fail(int code = 0, IList<T> data = null, string message = null)
        {
            return new ListResult<T>(
                success: false,
                code: code,
                data: data,
                message: message);
        }
    }

    public class PageResult<T> : ListResult<T>, IPageResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public PageResult() { }

        public PageResult(int pageIndex, int pageSize, int totalPages, string message = null, bool success = true, int code = 0, IList<T> data = null)
            : base(success, code, message, data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public PageResult(int pageIndex, int pageSize, int totalPages, int totalItems, string message = null, bool success = true, int code = 0, IList<T> data = null)
            : base(success, code, message, data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalItems = totalItems;
        }

        public static IResult Ok(int pageIndex, int pageSize, int totalPages, int totalItems, string message = null, int code = 0, IList<T> data = null)
        {
            return new PageResult<T>(
                pageIndex: pageIndex,
                pageSize: pageSize,
                totalPages: totalPages,
                totalItems: totalItems,
                message: message,
                success: true,
                code: code,
                data: data);
        }


        public static IResult Fail(int pageIndex, int pageSize, int totalPages, int totalItems, string message, int code = 0, IList<T> data = null)
        {
            return new PageResult<T>(
                pageIndex: pageIndex,
                pageSize: pageSize,
                totalPages: totalPages,
                totalItems: totalItems,
                success: false,
                message: message,
                code: code,
                data: data);
        }

    }

    public class ExceptionResult<T> : Result<T>, IExceptionResult<T>
    {
        public IList<Exception> Errors { get; set; }

        public ExceptionResult(Exception exception, T data = default(T), int code = 0, string message = null) : base(success: false, code: code, data: data, message: message ?? exception.Message)
        {
            Errors = new List<Exception> { exception };
        }

        public ExceptionResult(IList<Exception> exceptions, T data = default(T), int code = 0, string message = null) : base(success: false, data: data, code: code, message: message)
        {
            Errors = exceptions;
        }
    }

    public class ExceptionResult : Result, IExceptionResult
    {
        public IList<Exception> Errors { get; set; }


        public ExceptionResult(Exception exception, int code = 0, string message = null) : base(success: false, code: code, message: message ?? exception.Message)
        {
            Errors = new List<Exception> { exception };
        }

        public ExceptionResult(IList<Exception> exceptions, int code = 0, string message = null) : base(success: false, code: code, message: message)
        {
            Errors = exceptions;
        }
    }
}
