using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Application.ViewModels;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
using FytSoa.Infra.Common.Extensions;
using LinqKit;

namespace FytSoa.Application.Services {
    public class SysMenuService : ISysMenuService {
        private readonly ISysMenuRepository _sysMenuRepository;
        public SysMenuService (ISysMenuRepository sysMenuRepository) {
            _sysMenuRepository = sysMenuRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenu>>> GetAll (PageParam param) {
            var result = JResult<List<SysMenu>>.Success ();
            try {
                var where = PredicateBuilder.New<SysMenu> (m => true);
                if (!string.IsNullOrEmpty (param.key)) {
                    where.And (m => m.Name.Contains (param.key));
                }
                if (!string.IsNullOrEmpty (param.status)) {
                    where.And (m => m.Status == bool.Parse (param.status));
                }
                result.Data = await _sysMenuRepository.GetListAsync (where, m => m.Sort, 2);
                return result;
            } catch (Exception ex) {
                return JResult<List<SysMenu>>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add (SysMenu model) {
            var result = JResult<int>.Success ();
            try {
                model.Id = Unique.Id ();
                if (model.ParentId != 0) {
                    var _model = await _sysMenuRepository.GetModelAsync (m => m.Id == model.ParentId);
                    model.Layer = _model.Layer + 1;
                    model.ParentIdList = _model.ParentIdList + model.Id.ToString () + ",";
                } else {
                    model.ParentIdList = model.Id.ToString () + ',';
                }
                var upModel = await _sysMenuRepository.GetFirstAsync (m => true, m => m.Sort, 1);
                if (upModel != null) {
                    model.Sort = upModel.Sort + 1;
                }
                result.Data = await _sysMenuRepository.AddAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Update (SysMenu model) {
            var result = JResult<int>.Success ();
            try {
                if (model.ParentId != 0) {
                    var _model = await _sysMenuRepository.GetModelAsync (m => m.Id == model.ParentId);
                    model.Layer = _model.Layer + 1;
                    model.ParentIdList = _model.ParentIdList + model.Id.ToString () + ",";
                } else {
                    model.ParentIdList = model.Id.ToString () + ',';
                }
                result.Data = await _sysMenuRepository.UpdateAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Delete (string ids) {
            var result = JResult<int>.Success ();
            try {
                result.Data = await _sysMenuRepository.DeleteAsync (m => m.ParentIdList.Contains (ids));
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 自定义排序
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<int>> ColSort (SortParam param) {
            var result = JResult<int>.Success ();
            try {
                int a = 0, b = 0, c = 0;
                var list = await _sysMenuRepository.GetListAsync (m => m.ParentId == param.Parent, m => m.Sort, 2);
                if (list.Count > 0) {
                    var index = 0;
                    foreach (var item in list) {
                        index++;
                        if (index == 1) {
                            if (item.Id == param.Id) //判断是否是头如果上升则不做处理
                            {
                                if (param.type == 1) //下降一位
                                {
                                    a = Convert.ToInt32 (item.Sort);
                                    b = Convert.ToInt32 (list[index].Sort);
                                    c = a;
                                    a = b;
                                    b = c;
                                    item.Sort = a;
                                    await _sysMenuRepository.UpdateAsync (item);
                                    var nitem = list[index];
                                    nitem.Sort = b;
                                    await _sysMenuRepository.UpdateAsync (nitem);
                                    break;
                                }
                            }
                        } else if (index == list.Count) {
                            if (item.Id == param.Id) //最后一条如果下降则不做处理
                            {
                                if (param.type == 0) //上升一位
                                {
                                    a = Convert.ToInt32 (item.Sort);
                                    b = Convert.ToInt32 (list[index - 2].Sort);
                                    c = a;
                                    a = b;
                                    b = c;
                                    item.Sort = a;
                                    await _sysMenuRepository.UpdateAsync (item);
                                    var nitem = list[index - 2];
                                    nitem.Sort = b;
                                    await _sysMenuRepository.UpdateAsync (nitem);
                                    break;
                                }
                            }
                        } else {
                            if (item.Id == param.Id) //判断是否是头如果上升则不做处理
                            {
                                if (param.type == 1) //下降一位
                                {
                                    a = Convert.ToInt32 (item.Sort);
                                    b = Convert.ToInt32 (list[index].Sort);
                                    c = a;
                                    a = b;
                                    b = c;
                                    item.Sort = a;
                                    await _sysMenuRepository.UpdateAsync (item);
                                    var nitem = list[index];
                                    nitem.Sort = b;
                                    await _sysMenuRepository.UpdateAsync (nitem);
                                    break;
                                } else {
                                    a = Convert.ToInt32 (item.Sort);
                                    b = Convert.ToInt32 (list[index - 2].Sort);
                                    c = a;
                                    a = b;
                                    b = c;
                                    item.Sort = a;
                                    await _sysMenuRepository.UpdateAsync (item);
                                    var nitem = list[index - 2];
                                    nitem.Sort = b;
                                    await _sysMenuRepository.UpdateAsync (nitem);
                                    break;
                                }
                            }
                        }
                    }
                }
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }
        public void Dispose () {
            GC.SuppressFinalize (this);
        }
    }
}