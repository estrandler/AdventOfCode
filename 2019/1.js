const fs = require('fs');

fs.readFile("1.txt", "utf8", (error, data) => {
    const input = data.split('\r\n').map(val => parseInt(val, 10));
    
    const solution = input.reduce((prev, current) => {
        return prev + (Math.floor(current / 3) - 2);
    }, 0);

    console.log('1a', solution);
});

const getFuelRecursive = (value, total) => {
    const fuel = Math.floor(value / 3) - 2;
    if(fuel < 1)
        return total;

    return getFuelRecursive(fuel, total + fuel);
}

fs.readFile("1.txt", "utf8", (error, data) => {
    const input = data.split('\r\n').map(val => parseInt(val, 10));
    const solution = input.reduce((prev, current) => getFuelRecursive(current, prev), 0);

    console.log('1b', solution);
});