// src/operators.test.js

// import your functions using destructuring
// and CommonJS require
const {
    add,
    subtract,
    divide,
    multiply
} = require('./operators');

// test the add function
test('adds 1 + 2 to equal 3', () => {
    const v = add(1, 2);
    expect(v).toBe(3);
});