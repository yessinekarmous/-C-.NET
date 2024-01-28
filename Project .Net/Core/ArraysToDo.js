function pushFront(arr, val) {
    for (let i = arr.length; i > 0; i--) {
        arr[i] = arr[i - 1];
    }
    arr[0] = val;
    return arr;
}

function popFront(arr) {
    const removedValue = arr[0];

    for (let i = 0; i < arr.length - 1; i++) {
        arr[i] = arr[i + 1];
    }
    arr.length--;
    console.log(removedValue + " returned, with " + arr + " printed in the function");
    return removedValue;
}

function insertAt(arr, ind, val) {
    for (let i = arr.length; i > ind; i--) {
        arr[i] = arr[i - 1]
    }
    arr[ind] = val
    return arr;
}

console.log(pushFront([5, 7, 2, 3], 8));
console.log(pushFront([99], 7));

console.log(popFront([0, 5, 10, 15]));
console.log(popFront([4, 5, 7, 9]));

console.log(insertAt([100, 200, 5], 2, 311));
console.log(insertAt([9, 33, 7], 1, 42));
