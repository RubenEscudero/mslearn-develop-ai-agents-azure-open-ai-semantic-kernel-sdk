using Microsoft.SemanticKernel;
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
string language = "French";
string history = @"I'm traveling with my kids and one of them has a penaut allergy.";

string prompt = $@"You are a travel assistant. You are helpful, creative, and very friendly. 
    Consider the traveler's background: ${history}

    Create a list of helpul phrases and words in ${language} a traveler would find useful.

    Group phrases by category. Include common direction words. 
    Display the phrases in the following format: 
    Hello - Ciao [chow]

    Begin with: 'Here are some phrases in ${language} you may find helpful:' 
    and end with: 'I hope this helps you on your trip!'";

var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result);