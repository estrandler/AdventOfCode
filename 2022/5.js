const fs = require("fs");

const map = { 1: 1, 5: 2, 9: 3, 13: 4, 17: 5, 21: 6, 25: 7, 29: 8, 33: 9 };

const { state_a, state_b, instructions } = fs
  .readFileSync("5.txt")
  .toString()
  .split("\r\n")
  .reduce(
    (prev, curr) => {
      if (curr.indexOf("[") > -1) {
        let takeNext = false;
        for (let i = 0; i < curr.length; i++) {
          const char = curr[i];

          if (takeNext) {
            prev.state_a[map[i]] = prev.state_a[map[i]] || [];
            prev.state_a[map[i]].push(char);
            prev.state_b[map[i]] = prev.state_b[map[i]] || [];
            prev.state_b[map[i]].push(char);
            takeNext = false;
          }

          if (char === "[") {
            takeNext = true;
          }
        }
      }

      if (curr.indexOf("move") === 0) {
        const instruction = curr.split(" ");
        prev.instructions.push({
          quantity: parseInt(instruction[1]),
          from: instruction[3],
          to: instruction[5],
        });
      }

      return prev;
    },
    { state_a: {}, state_b: {}, instructions: [] }
  );

const move = (source, target, amount) =>
  source.splice(0, amount).concat(target);

instructions.forEach((instruction) => {
  for (let i = 1; i <= instruction.quantity; i++) {
    state_a[instruction.to] = move(
      state_a[instruction.from],
      state_a[instruction.to],
      1
    );
  }

  state_b[instruction.to] = move(
    state_b[instruction.from],
    state_b[instruction.to],
    instruction.quantity
  );
});

console.log(
  "5a",
  Object.values(state_a)
    .map((arr) => arr[0])
    .join("")
);

console.log(
  "5b",
  Object.values(state_b)
    .map((arr) => arr[0])
    .join("")
);
