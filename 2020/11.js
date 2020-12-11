const fs = require("fs");
const rows = fs.readFileSync("11.txt").toString().split("\r\n");

const adjacent = (rows, x, y) => {
  return [
    { x: x - 1, y: y },
    { x: x + 1, y: y },
    { x: x - 1, y: y - 1 },
    { x: x + 1, y: y - 1 },
    { x: x, y: y - 1 },
    { x: x, y: y + 1 },
    { x: x + 1, y: y + 1 },
    { x: x - 1, y: y + 1 },
  ]
    .filter(
      ({ x, y }) => x >= 0 && x < rows[0].length && y >= 0 && y < rows.length
    )
    .map(({ x, y }) => rows[y][x]);
};
const countOccupied = (arr) => {
  const count = arr
    .join("")
    .split("")
    .filter((status) => {
      return status === "#";
    }).length;

  return count;
};

const hasNoOccupied = (adjacentSeats) => {
  return adjacentSeats.filter((status) => status === "#").length === 0;
};
const hasAtLeast4Occupied = (adjacentSeats) =>
  adjacentSeats.filter((status) => status === "#").length > 3;

const swapSeats = (rs) => {
  const result = [];
  for (let y = 0; y < rs.length; y++) {
    const row = rs[y];
    for (let x = 0; x < row.length; x++) {
      const seat = row[x];
      const a = adjacent(rs, x, y);

      result[y] = (result[y] || "")
        .split("")
        .concat(
          seat === "L" && hasNoOccupied(a)
            ? "#"
            : seat === "#" && hasAtLeast4Occupied(a)
            ? "L"
            : seat
        )
        .join("");
    }
  }

  return result;
};

let current = swapSeats(rows);
let count = 0;

do {
  count = countOccupied(current);
  current = swapSeats(current);
} while (count !== countOccupied(current));

console.log("A", countOccupied(current));
