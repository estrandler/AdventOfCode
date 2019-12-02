const fs = require('fs');

const getValuesForOperation = (index, input) => {
    return {
        a: input[input[index + 1]],
        b: input[input[index + 2]],
        targetIndex: input[index + 3]
    }
};

fs.readFile("2.txt", "utf8", (error, data) => {
    const input = data.split(',').map(val => parseInt(val, 10));
    input[1] = 12;
    input[2] = 2;

    for (let i = 0; i < input.length; i = i + 4) {
        const instruction = input[i];

        if (instruction === 99)
            break;

        if (instruction === 1) {
            const { a, b, targetIndex } = getValuesForOperation(i, input);
            input[targetIndex] = a + b;
        }

        if (instruction === 2) {

            const { a, b, targetIndex } = getValuesForOperation(i, input);
            input[targetIndex] = a * b;
        }
    }

    console.log('2a', input[0]);
});

fs.readFile("2.txt", "utf8", (error, data) => {
    const inputFromFile = data.split(',').map(val => parseInt(val, 10));
    let solution;
    for (let i0 = 0; i0 < 100; i0++) {
        for (let i1 = 0; i1 < 100; i1++) {
            const input = Object.assign([], inputFromFile)
            input[1] = i0;
            input[2] = i1;

            for (let i = 0; i < input.length; i = i + 4) {
                const instruction = input[i];

                if (instruction === 99)
                    break;

                if (instruction === 1) {
                    const { a, b, targetIndex } = getValuesForOperation(i, input);
                    input[targetIndex] = a + b;
                }

                if (instruction === 2) {

                    const { a, b, targetIndex } = getValuesForOperation(i, input);
                    input[targetIndex] = a * b;
                }

                if(input[0] === 19690720) {
                    solution = 100 * i0 + i1;
                    break;
                }
            }
        }
    }

    console.log('2b', solution);
});