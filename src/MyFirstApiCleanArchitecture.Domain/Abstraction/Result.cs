﻿namespace MyFirstApiCleanArchitecture.Domain.Abstraction;

public class Result<TDto> where TDto : IResult
{
    public TDto? Data { get; set; }

    public bool IsNotSuccessfull { get; set; }

    public int StatusCode { get; set; }

    public Dictionary<string, string>? Errors { get; set; }

    //Success
    private Result(TDto? data, int statusCode)
    {
        Data = data;
        IsNotSuccessfull = false;
        StatusCode = statusCode;
    }

    //Succes without data
    private Result(int statusCode)
    {
        IsNotSuccessfull = false;
        StatusCode = statusCode;
    }

    //Fail with one error
    private Result(int statusCode, string errorCode, string errorMessage)
    {
        IsNotSuccessfull = true;
        StatusCode = statusCode;
        Errors = new()
        {
            { errorCode, errorMessage }
        };
    }

    //Fail with many errors
    private Result(int statusCode, Dictionary<string, string> errors)
    {
        IsNotSuccessfull = true;
        StatusCode = statusCode;
        Errors = errors;
    }

    //method that return new Result
    public static Result<TDto> Success(
        TDto data,
        int statusCode)
    => new(data, statusCode);

    public static Result<TDto> Success(
        int statusCode)
    => new(statusCode);

    public static Result<TDto> Failed(
         int statusCode,
         string errorCode,
         string errorMessage)
    => new(statusCode, errorCode, errorMessage);

    public static Result<TDto> Failed(
        int statusCode, Dictionary<string, string> errors)
    => new(statusCode, errors);
}

public class NoContentDto : IResult;
