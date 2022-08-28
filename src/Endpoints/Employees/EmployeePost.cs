﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employee;

public class EmployeePost
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser {UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result =  userManager.CreateAsync(user, employeeRequest.Password).Result;
        
        var userClams = new List<Claim>()
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name)

        };

        if (!result.Succeeded) return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var clamResult = userManager.AddClaimsAsync(user, userClams).Result;

        if (!result.Succeeded) return Results.ValidationProblem(clamResult.Errors.ConvertToProblemDetails());

        return Results.Created($"/employees/{user.Id}", user.Id);
        
    }
    
}
