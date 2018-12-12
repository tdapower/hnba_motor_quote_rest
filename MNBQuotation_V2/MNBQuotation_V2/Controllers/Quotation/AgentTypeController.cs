using MNBQuotation_V2.Models.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MNBQuotation_V2.Controllers.Quotation
{
    public class AgentTypeController : ApiController
    {
        // GET api/agenttype
        public IEnumerable<AgentType> Get()
        {
            List<AgentType> agentTypeList = new List<AgentType>();
            agentTypeList.Add(new AgentType() { AgentTypeName = "Agent" });
            agentTypeList.Add(new AgentType() { AgentTypeName = "Broker" });
            agentTypeList.Add(new AgentType() { AgentTypeName = "Direct" });
            agentTypeList.Add(new AgentType() { AgentTypeName = "HNB" });
            agentTypeList.Add(new AgentType() { AgentTypeName = "HNB Grameen" });
            agentTypeList.Add(new AgentType() { AgentTypeName = "Direct Special" });
            agentTypeList.Add(new AgentType() { AgentTypeName = "Staff" });


            return agentTypeList;
        }

      
    }
}
