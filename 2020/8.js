const fs = require("fs");
const instructions = fs
  .readFileSync("8.txt")
  .toString()
  .split("\r\n")
  .map((instruction, index) => ({
    eval: instruction.split(" ")[1],
    original: instruction.split(" ")[0],
    swapped: instruction.split(" ")[0] === "jmp" ? "nop" : "jmp",
    index,
  }));

const computePartOne = () => {
  let acc = 0;
  let nextIndex = 0;
  const visitedIndexes = [];

  while (true) {
    if (!!visitedIndexes.find((value) => value === nextIndex)) {
      break;
    }

    visitedIndexes.push(nextIndex);

    const instruction = instructions[nextIndex];

    if (instruction.original === "acc") {
      acc = acc + eval(instruction.eval);
      nextIndex++;
    } else if (instruction.original === "jmp") {
      nextIndex = nextIndex + eval(instruction.eval);
    } else {
      nextIndex++;
    }
  }

  console.log("A", acc);
};

const computePartTwo = () => {
  const instructionsEligibleForChange = instructions.filter(
    (instruction) =>
    (instruction.original === "nop" && instruction.eval[0] === "+") || instruction.original === "jmp"
  ).map(({index}) => index);


  const results = [];
  for (let i = 0; i < instructionsEligibleForChange.length; i++) {
    let acc = 0;
    let nextIndex = 0;
    const visitedIndexes = [];
    const instructionsToRun = instructions.map((instruction, index) => index !== instructionsEligibleForChange[i] ? instruction : ({
        original: instruction.swapped,
        swapped: instruction.swapped,
        eval: instruction.eval,
        index: instruction.index
    }));

    while (nextIndex < instructions.length) {
      if (visitedIndexes.find((value) => value === nextIndex) !== undefined) {
        acc = -1;
        break;
      }
      visitedIndexes.push(nextIndex);

      const instruction = instructionsToRun[nextIndex];

      if (instruction.original === "acc") {
        acc = acc + eval(instruction.eval);
        nextIndex++;
      } else if (instruction.original === "jmp") {
        nextIndex = nextIndex + eval(instruction.eval);
      } else {
        nextIndex++;
      }
    }

    results.push(acc);
  }

  console.log("B", results.filter(acc => acc !== -1));
};

computePartOne();
computePartTwo();
