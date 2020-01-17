using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.Dto;
using Test.Core.IOC;

namespace Test.Core.Tree
{
    /// <summary>
    /// TreeUtil
    /// </summary>
    public interface ITreeUtil : IDependency 
    {
        /// <summary>
        /// Util.GetTreeFromBaseDtoList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Ttree"></typeparam>
        /// <param name="dto"></param>
        /// <param name="tree"></param>
        /// <param name="list"></param>
        void GetDtoTree<T, Ttree>(T dto, BaseTreeDto<Ttree> tree, List<T> list) where T : BaseDto, ITreeDto, new() where Ttree : BaseTreeDto<Ttree>, new();

        void GetDtoTrees<T, Ttree>(List<T> list, int rootId, ref List<Ttree> treeList) where T : BaseDto, ITreeDto, new() where Ttree : BaseTreeDto<Ttree>, ITreeDto, new();

        void GetDtoTrees<T, Ttree>(List<T> list, List<int> rootIds, ref List<Ttree> treeList) where T : BaseDto, ITreeDto, new() where Ttree : BaseTreeDto<Ttree>, ITreeDto, new();

        /// <summary>
        /// Util.GetTreeFromBaseDtoList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Ttree"></typeparam>
        /// <param name="list"></param>
        /// <param name="treeList"></param>
        void GetDtoTrees<T, Ttree>(List<T> list, ref List<Ttree> treeList) where T : BaseDto, ITreeDto, new() where Ttree : BaseTreeDto<Ttree>, ITreeDto, new();
    }
}
