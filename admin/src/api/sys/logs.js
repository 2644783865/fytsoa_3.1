import request from '@/utils/request'
export function getList(params) {
  return request({
    url: 'api/logs',
    method: 'get',
    params,
  })
}
export function getModel(params) {
  return request({
    url: 'api/logs/' + params,
    method: 'get',
  })
}
export function deletes(data) {
  return request({
    url: 'api/logs/' + data,
    method: 'delete',
  })
}
