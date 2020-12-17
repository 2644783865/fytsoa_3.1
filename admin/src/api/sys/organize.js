import request from '@/utils/request'
export function getList(params) {
  return request({
    url: 'api/organize',
    method: 'get',
    params,
  })
}
export function add(data) {
  return request({
    url: 'api/organize',
    method: 'post',
    data,
  })
}
export function update(data) {
  return request({
    url: 'api/organize',
    method: 'put',
    data,
  })
}
export function deletes(data) {
  return request({
    url: 'api/organize/' + data,
    method: 'delete',
  })
}
