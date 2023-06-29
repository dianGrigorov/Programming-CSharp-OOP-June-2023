using ShoppingSpree.Models;

List<Person> people = new List<Person>();
List<Product> products = new List<Product>();

try
{
	string[] nameMoneyTokens = Console.ReadLine()
		.Split(";", StringSplitOptions.RemoveEmptyEntries);

	foreach (string token in nameMoneyTokens)
	{
		string[] nameMoney = token.Split("=", StringSplitOptions.RemoveEmptyEntries);
		Person perosn = new(nameMoney[0], decimal.Parse(nameMoney[1]));
		people.Add(perosn);
	}

	string[] productCostTonens = Console.ReadLine()
		.Split(";", StringSplitOptions.RemoveEmptyEntries);

	foreach (var token in productCostTonens)
	{
		string[] currProduct = token.Split("=", StringSplitOptions.RemoveEmptyEntries);

		Product product = new(currProduct[0], decimal.Parse(currProduct[1]));
		products.Add(product);
	}
}
catch (ArgumentException ex)
{

    Console.WriteLine(ex.Message);
	return;
}

string input;
while ((input = Console.ReadLine()) != "END")
{
	string[] personProduct = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

	string personName = personProduct[0];
	string productName = personProduct[1];

	Person person = people.FirstOrDefault(p => p.Name == personName);
	Product product = products.FirstOrDefault(p => p.Name == productName);
	if (person is not null && product is not null)
	{
        Console.WriteLine(person.Add(product));
    }
}

Console.WriteLine(string.Join(Environment.NewLine, people));