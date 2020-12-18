import request from '@/utils/request'
export function getList(params) {
  return request({
    url: 'api/post',
    method: 'get',
    params,
  })
}
export function add(data) {
  return request({
    url: 'api/post',
    method: 'post',
    data,
  })
}
export function update(data) {
  return request({
    url: 'api/post',
    method: 'put',
    data,
  })
}
export function deletes(data) {
  return request({
    url: 'api/post/' + data,
    method: 'delete',
  })
}
