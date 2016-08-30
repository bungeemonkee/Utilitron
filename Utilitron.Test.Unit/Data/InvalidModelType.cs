using Utilitron.Data;

namespace Utilitron.Test.Unit.Data
{
    public class InvalidModelType : Model<int, ValidModelType>
    {
        public int Id { get; set; }

        public override int GetIdentifier() => Id;
    }
}
