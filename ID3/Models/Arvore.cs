using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ID3.Models
{
    public class Arvore<T>
    {
        public TreeNode<T> Root { get; set; }
    }
}
