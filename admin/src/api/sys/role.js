import request from '@/utils/request'
export function getList(params) {
  return request({
    url: 'api/role',
    method: 'get',
    params,
  })
}
export function add(data) {
  return request({
    url: 'api/role',
    method: 'post',
    data,
  })
}
export function update(data) {
  return request({
    url: 'api/role',
    method: 'put',
    data,
  })
}
export function deletes(data) {
  return request({
    url: 'api/role/' + data,
    method: 'delete',
  })
}
