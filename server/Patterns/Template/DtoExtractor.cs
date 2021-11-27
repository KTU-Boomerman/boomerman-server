using BoomermanServer.Patterns.Facade;

namespace BoomermanServer.Patterns.Template
{
    public abstract class DtoExtractor
    {
        protected ManagerFacade _facade;

        public DtoExtractor(ManagerFacade facade)
        {
            _facade = facade;
        }

        public object Extract()
        {
            var data = ExtractData();
            return FormDto(data);
        }

        public abstract object ExtractData();

        public abstract object FormDto(object data);
    }
}
