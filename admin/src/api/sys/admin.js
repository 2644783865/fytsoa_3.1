import request from '@/utils/request'
export function getList(params) {
  return request({
    url: 'api/admin',
    method: 'get',
    params,
  })
}
export function add(data) {
  return request({
    url: 'api/admin',
    method: 'post',
    data,
  })
}
export function update(data) {
  return request({
    url: 'api/admin',
    method: 'put',
    data,
  })
}
export function deletes(data) {
  return request({
    url: 'api/admin/' + data,
    method: 'delete',
  })
}

export function postAll(params) {
  return request({
    url: 'api/post',
    method: 'get',
    params,
  })
}

export function roleAll(params) {
  return request({
    url: 'api/role',
    method: 'get',
    params,
  })
}

export function organizeAll(params) {
  return request({
    url: 'api/organize',
    method: 'get',
    params,
  })
}
