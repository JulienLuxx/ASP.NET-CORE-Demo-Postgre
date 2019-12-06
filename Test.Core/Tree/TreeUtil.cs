using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Core.Dto;

namespace Test.Core.Tree
{
    /// <summary>
    /// TreeUtil
    /// </summary>
    public class TreeUtil: ITreeUtil
    {
        private IMapper _mapper;

        public TreeUtil(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// GetTreeForBaseDto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Ttree"></typeparam>
        /// <param name="dto"></param>
        /// <param name="tree"></param>
        /// <param name="list"></param>
        public void GetDtoTree<T, Ttree>(T dto, BaseTreeDto<Ttree> tree, List<T> list) where T : BaseDto, ITreeDto, new() where Ttree : BaseTreeDto<Ttree>, new()
        {
            try
            {
                if (null == dto)
                {
                    return;
                }
                tree = _mapper.Map(dto, tree);
                Func<T, bool> func = f => f.ParentId == dto.Id;
                var childs = list.Where(func).ToList();
                foreach (var child in childs)
                {
                    Ttree node = new Ttree();
                    tree.Childrens.Add(node);
                    GetDtoTree(child, node, list);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetTreeForBaseDto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Ttree"></typeparam>
        /// <param name="list"></param>
        /// <param name="rootId"></param>
        /// <param name="treeList"></param>
        public void GetDtoTrees<T, Ttree>(List<T> list, int rootId ,ref List<Ttree> treeList) where T : BaseDto, ITreeDto, new() where Ttree : BaseTreeDto<Ttree>,ITreeDto, new()
        {
            try
            {
                var dict = new Dictionary<int, Ttree>();
                list.ForEach(f => dict.Add(f.Id, _mapper.Map<Ttree>(f)));
                foreach (var item in dict.Values)
                {
                    if (item.ParentId == rootId)
                    {
                        treeList.Add(item);
                    }
                    else
                    {
                        if (dict.ContainsKey(item.ParentId))
                        {
                            dict[item.ParentId].Childrens.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
