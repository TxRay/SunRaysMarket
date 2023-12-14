export function get(key) {
    const result = sessionStorage.getItem(key)
    console.log("js session get: " + key + " : " + result);
    return result
}

export function set(key, value) {
    console.log("js session set: " + key + " : " + value);
    window.sessionStorage.setItem(key, value)
}

export function remove(key) {
    window.sessionStorage.removeItem(key)
}

export function clear() {
    window.sessionStorage.clear()
}