namespace Splitify.BuildingBlocks.Domain
{
    public class Entity
    {
        public string Id { get; }

        public Entity(string id)
        {
            EnsureIdIsValid(id);
            Id = id;

        }

        private static void EnsureIdIsValid(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (id.Length < 1)
            {
                throw new ArgumentException("length was less than 1", nameof(id));
            }
        }
    }
}