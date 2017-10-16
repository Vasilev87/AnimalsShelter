using Bytes2you.Validation;

namespace AnimalsShelter.Data.SaveContext
{
    public class SaveContext : ISaveContext
    {
        private readonly MsSqlDbContext context;

        public SaveContext(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, nameof(context)).IsNull().Throw();

            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
