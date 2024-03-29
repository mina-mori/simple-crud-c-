
namespace Application.Helpers
{
    public static class StringHelper
    {
        public static string AlphabetizeUserName(string userName)
        {
            var index = userName.IndexOf(" ");
            // Split the name into an array of characters
            char[] characters = userName.ToLower().Replace(" ","").ToCharArray();

            // Sort the characters alphabetically
            Array.Sort(characters);

            // Convert the sorted characters back to a string
            string sortedName = new string(characters);
            var name = sortedName.Insert(index, " ");

            return name;
        }
    }
}
