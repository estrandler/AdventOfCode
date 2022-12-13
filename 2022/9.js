const fs = require("fs");

const execute = (dir, target) => {
  if (dir === "U") {
    target.y = target.y + 1;
  }
  if (dir === "D") {
    target.y = target.y - 1;
  }
  if (dir === "L") {
    target.x = target.x - 1;
  }
  if (dir === "R") {
    target.x = target.x + 1;
  }

  return target;
};

const { visited } = fs
  .readFileSync("9.txt")
  .toString()
  .split("\r\n")
  .map((row) => ({
    dir: row.split(" ")[0],
    amount: parseInt(row.split(" ")[1]),
  }))
  .reduce(
    (prev, curr) => {
      for (let i = 0; i < curr.amount; i++) {
        prev.headPosition = execute(curr.dir, prev.headPosition);

        let differenceX = prev.headPosition.x - prev.tailPosition.x;
        let differenceXabs = Math.abs(differenceX);
        let differenceY = prev.headPosition.y - prev.tailPosition.y;
        let differenceYabs = Math.abs(differenceY);

        //Find if need diagonal hoist
        if (
          (differenceXabs > 1 && differenceYabs > 0) ||
          (differenceYabs > 1 && differenceXabs > 0)
        ) {
          prev.tailPosition = execute(
            differenceX < 0 ? "L" : "R",
            prev.tailPosition
          );
          prev.tailPosition = execute(
            differenceY < 0 ? "D" : "U",
            prev.tailPosition
          );
        } else {
          if (differenceXabs > 1) {
            prev.tailPosition = execute(
              differenceX < 0 ? "L" : "R",
              prev.tailPosition
            );
          }

          if (differenceYabs > 1) {
            prev.tailPosition = execute(
              differenceY < 0 ? "D" : "U",
              prev.tailPosition
            );
          }
        }

        const currentPlace = `${prev.tailPosition.x},${prev.tailPosition.y}`;
        prev.visited[currentPlace] = (prev.visited[currentPlace] || 0) + 1;
      }
      return prev;
    },
    {
      visited: { "0,0": 1 },
      headPosition: { x: 0, y: 0 },
      tailPosition: { x: 0, y: 0 },
    }
  );

console.log("a", Object.keys(visited).length); //
