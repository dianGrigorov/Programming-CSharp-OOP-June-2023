using FootballTeamGenerator.Models;

List<Team> teams = new List<Team>();
string input;

while ((input = Console.ReadLine()) != "END")
{
    string[] tokens = input.Split(";", StringSplitOptions.RemoveEmptyEntries);
    string command = tokens[0];

    try
    {
        if (command == "Team")
        {
            AddTeam(tokens[1], teams);
        }
        else if (command == "Add")
        {

            AddPlayer(
                    tokens[1],
                    tokens[2],
                    int.Parse(tokens[3]),
                    int.Parse(tokens[4]),
                    int.Parse(tokens[5]),
                    int.Parse(tokens[6]),
                    int.Parse(tokens[7])
                    );
        }
        else if (command == "Remove")
        {
            RemovePlayer(tokens[1], tokens[2]);
        }
        else if (command == "Rating")
        {
            PrintRating(tokens[1]);
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }
}

void AddTeam(string name, List<Team> teams)
{
    teams.Add(new Team(name));
}

void AddPlayer(
    string teamName,
    string playerName,
    int endurance,
    int sprint,
    int dribble,
    int passing,
    int shooting
    )
{
    Player player = new(playerName, endurance, sprint, dribble, passing, shooting);
   
    Team team = teams.FirstOrDefault(t => t.Name == teamName);
    if (team == null)
    {
        throw new ArgumentException($"Team {teamName} does not exist.");
    }
    team.AddPlayer(player);
}
void RemovePlayer(string teamName, string playerName)
{
    Team team = teams.FirstOrDefault(t => t.Name == teamName);
    if (team == null)
    {
        throw new ArgumentException($"Team {teamName} does not exist.");
    }
    team.RemovePlayer(playerName);
}
void PrintRating(string teamName)
{
    Team team = teams.FirstOrDefault(t => t.Name == teamName);
    if (team == null)
    {
        throw new ArgumentException($"Team {teamName} does not exist.");
    }
    Console.WriteLine($"{teamName} - {team.Rating:f0}");
}