using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain2
{
    public enum BlockType
    {
        BLOCK_FUNC,
        BLOCK_STRING,
        BLOCK_CALC
    }

    public class BlockInfo
    {
        public BlockType block_type;

    }
}
