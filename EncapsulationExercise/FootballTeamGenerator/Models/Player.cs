namespace FootballTeamGenerator.Models;

public class Player
{
    private const int MinStats = 0;
    private const int MaxStats = 100;

    private string name;

    private int endurance;
    private int sprint;
    private int dribble;
    private int passing;
    private int shooting;

    public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
    {
        Name = name;
        Endurance = endurance;
        Sprint = sprint;
        Dribble = dribble;
        Passing = passing;
        Shooting = shooting;
    }
    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("A name should not be empty.");
            }
            name = value;
        }
    }

    public int Endurance
    {
        get => endurance;
        set
        {
            if (!ValidStats(value))
            {
                throw new ArgumentException($"{nameof(Endurance)} should be between 0 and 100.");
            }
            endurance = value;
        }
    }
    public int Sprint
    {
        get => sprint;
        set
        {
            if (!ValidStats(value))
            {
                throw new ArgumentException($"{nameof(Sprint)} should be between 0 and 100.");
            }
            sprint = value;
        }
    }

    public int Dribble
    {
        get => dribble;
        set
        {
            if (!ValidStats(value))
            {
                throw new ArgumentException($"{nameof(Dribble)} should be between 0 and 100.");
            }
            dribble = value;
        }
    }
    public int Passing
    {
        get => passing;
        set
        {
            if (!ValidStats(value))
            {
                throw new ArgumentException($"{nameof(Passing)} should be between 0 and 100.");
            }
            passing = value;
        }
    }
    public int Shooting
    {
        get => shooting;
        set
        {
            if (!ValidStats(value))
            {
                throw new ArgumentException($"{nameof(Shooting)} should be between 0 and 100.");
            }
            shooting = value;
        }
    }

    public double Stats
    {
        get => (Endurance + Sprint + Dribble + Passing + Shooting) / 5.0;
    }
    private bool ValidStats(int value)
    {
        if (value > MinStats && value < MaxStats)
        {
            return true;
        }
        return false;
    }
}
