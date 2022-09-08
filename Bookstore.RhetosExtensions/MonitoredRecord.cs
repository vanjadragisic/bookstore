using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using System.ComponentModel.Composition;

namespace Bookstore.RhetosExtensions
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("MonitoredRecord")]
    public class MonitoredRecordInfo : EntityInfo
    {

    }

    [Export(typeof(IConceptMacro))]
    public class MonitoredRecordMacro : IConceptMacro<MonitoredRecordInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(MonitoredRecordInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            newConcepts.Add(new EntityLoggingInfo
            {
                Entity = conceptInfo
            });

            var createdAt = new DateTimePropertyInfo
            {
                DataStructure = conceptInfo,
                Name = "CreatedAt"
            };

            newConcepts.Add(createdAt);

            newConcepts.Add(new CreationTimeInfo
            {
                Property = createdAt
            });

            return newConcepts;
        }
    }
}