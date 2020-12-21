import request from '@/utils/request'
export function getList(params) {
  return request({
    url: 'api/menu',
    method: 'get',
    params,
  })
}
export function add(data) {
  return request({
    url: 'api/menu',
    method: 'post',
    data,
  })
}
export function colSort(data) {
  return request({
    url: 'api/menu/sort',
    method: 'post',
    data,
  })
}
export function update(data) {
  return request({
    url: 'api/menu',
    method: 'put',
    data,
  })
}
export function deletes(data) {
  return request({
    url: 'api/menu/' + data,
    method: 'delete',
  })
}
