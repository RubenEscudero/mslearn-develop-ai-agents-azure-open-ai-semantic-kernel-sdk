using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Plugins.Core;

/*
 * Conexión con Azure OpenAI
 */
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    "gpt-4o",
    "url",
    "key",
    "gpt-4o");
/*
 * Plugins
 */
#pragma warning disable SKEXP0050 // Este tipo se incluye solo con fines de evaluación y está sujeto a cambios o a que se elimine en próximas actualizaciones. Suprima este diagnóstico para continuar.
builder.Plugins.AddFromType<TimePlugin>();
builder.Plugins.AddFromType<ConversationSummaryPlugin>();

var kernel = builder.Build();

/*
 * Ejemplo de como obtener una consulta de texto
 */
//var result = await kernel.InvokePromptAsync(
//    "Give me a list of breakfast foods with eggs and cheese");
//Console.WriteLine(result);

/*
 * Ejemplo de como obtener una fecha utilizando un complemento
 * del SDK de kernel semántico con el paquete Plugins.Core
 */
//var currentDay = await kernel.InvokeAsync("TimePlugin", "DayOfWeek");
//Console.WriteLine(currentDay);

/*
 * Ejemplo de como a partir de una frase podemos obtener diferentes datos
 */
//string input = @"I'm a vegan in search of new recipes. I love spicy food! 
//Can you give me a list of breakfast recipes that are vegan friendly?";

//var result = await kernel.InvokeAsync(
//    "ConversationSummaryPlugin",
//    "SummarizeConversation",
//    new() {{ "input", input }});

//Console.WriteLine(result);

/*
 * Ejemplo para obtener resultados en distintos idiomas bajo un contexto
 */
//string language = "French";
//string history = @"I'm traveling with my kids and one of them has a penaut allergy.";

//string input = @"I have a vacation from June 1 to July 22. I want to go to Greece. 
//    I live in Chicago.";

//string prompt = @$"
//<message role=""system"">Instructions: Identify the from and to destinations 
//and dates from the user's request</message>

//<message role=""user"">Can you give me a list of flights from Seattle to Tokyo? 
//I want to travel from March 11 to March 18.</message>

//<message role=""assistant"">Seattle|Tokyo|03/11/2024|03/18/2024</message>

//<message role=""user"">${input}</message>";

//var result = await kernel.InvokePromptAsync(prompt);
//Console.WriteLine(result);

/*
 * Ejemplo de creación de un complemento semántico personalizado
 */
//var plugins = kernel.CreatePluginFromPromptDirectory("D:\\Proyectos\\mslearn-develop-ai-agents-azure-open-ai-semantic-kernel-sdk\\SKProject\\Prompts\\");
//string input = "G, C";

//var result = await kernel.InvokeAsync(
//    plugins["SuggestChords"],
//    new() {{ "startingChords" , input }});

//Console.WriteLine(result);

/*
 * Ejemplo de crear varios complementos semánticos
 */
var prompts = kernel.ImportPluginFromPromptDirectory("D:\\Proyectos\\mslearn-develop-ai-agents-azure-open-ai-semantic-kernel-sdk\\SKProject\\Prompts\\TravelPlugins\\");

ChatHistory history = [];
string input = @"I'm planning an anniversary trip with my spouse. We like hiking, 
    mountains, and beaches. Our travel budget is $15000";

var result = await kernel.InvokeAsync<string>(prompts["SuggestDestinations"],
    new() { { "input", input} });

Console.WriteLine(result);
history.AddUserMessage(input);
history.AddAssistantMessage(result);

Console.WriteLine("Where would you like to go?");
input = Console.ReadLine();

result = await kernel.InvokeAsync<string>(prompts["SuggestActivities"],
    new() { { "history", history },
        { "destination", input} });

Console.WriteLine(result);