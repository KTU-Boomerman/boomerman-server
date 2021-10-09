using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoomermanServer.Data
{
	public interface IDataTransferable<DTO>
	{
		DTO ToDTO();
	}
}