using UnityEngine;

public static class StringResolver
{
    public static string GetYearSuffix(int numberOfYears)
    {
        numberOfYears = Mathf.Abs(numberOfYears);

        int lastTwo = numberOfYears % 100;
        int lastOne = numberOfYears % 10;

        if (lastTwo >= 11 && lastTwo <= 14)
            return "���";

        return lastOne switch
        {
            1 => "���",
            2 or 3 or 4 => "����",
            _ => "���"
        };
    }
}
