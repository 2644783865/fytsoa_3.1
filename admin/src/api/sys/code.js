import request from '@/utils/request'
export function getCodeList(params) {
  return request({
    url: 'api/code',
    method: 'get',
    params,
  })
}
export function add(data) {
  return request({
    url: 'api/code',
    method: 'post',
    data,
  })
}
export function update(data) {
  return request({
    url: 'api/code',
    method: 'put',
    data,
  })
}
export function deletes(data) {
  return request({
    url: 'api/code/' + data,
    method: 'delete',
  })
}
