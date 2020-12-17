export function changeTree(data) {
  if (data.length > 0) {
    data.forEach((item) => {
      const parentId = item.parentId
      if (parentId) {
        data.forEach((ele) => {
          if (ele.id === parentId) {
            let childArray = ele.children
            if (!childArray) {
              childArray = []
            }

            childArray.push(item)
            ele.children = childArray
          }
        })
      }
    })
  }
  return data.filter((item) => item.parentId == '0')
}
