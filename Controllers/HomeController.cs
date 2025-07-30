using Microsoft.AspNetCore.Mvc;

namespace dotrestapiwithmongo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public ContentResult Get()
        {
            var html = @"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Student API</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', sans-serif;
            background: #f0f2f5;
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            color: #333;
        }

        h1 {
            font-size: 3rem;
            margin-bottom: 1rem;
        }

        .btn {
            padding: 12px 24px;
            font-size: 1rem;
            background-color: #0078D7;
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            text-decoration: none;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #005fa3;
        }
    </style>
</head>
<body>
    <h1>📚 Welcome to the Student API 🎓</h1>
    <a href='/swagger' target='_blank' class='btn'>🚀 Go to API Docs</a>
</body>
</html>
";
            return new ContentResult
            {
                Content = html,
                ContentType = "text/html"
            };
        }
    }
}
