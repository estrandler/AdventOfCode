const fs = require("fs");
const rows = fs.readFileSync("4.txt").toString().split("\r\n");

const requiredFields = [
  {
    key: "byr",
    condition: (value) => Number(value) >= 1920 && Number(value) <= 2002,
  },
  {
    key: "iyr",
    condition: (value) => Number(value) >= 2010 && Number(value) <= 2020,
  },
  {
    key: "eyr",
    condition: (value) => Number(value) >= 2020 && Number(value) <= 2030,
  },
  {
    key: "hgt",
    condition: (value) => {
      if (value.includes("in")) {
        const height = Number(value.split("in")[0]);
        return height >= 59 && height <= 76;
      }

      if (value.includes("cm")) {
        const height = Number(value.split("cm")[0]);
        return height >= 150 && height <= 193;
      }
    },
  },
  {
    key: "hcl",
    condition: (value) => RegExp("^#{1}[a-fA-f0-9]{6}").test(value),
  },
  {
    key: "ecl",
    condition: (value) =>
      ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"].find(
        (ecl) => ecl === value
      ),
  },
  { key: "pid", condition: (value) => RegExp("^[0-9]{9}$").test(value) },
];

const format = (prev, next) => {
  if (next !== "") {
    prev[0] = prev[0] += " " + next;
  } else {
    prev.unshift("");
  }
  return prev;
};

const A = rows
  .reduce(format, [""])
  .filter((pw) => requiredFields.every((field) => pw.indexOf(field.key) !== -1))
  .length;

const B = rows
  .reduce(format, [""])
  .filter((pw) => requiredFields.every((field) => pw.indexOf(field.key) !== -1))
  .filter((pw) => {
    return pw
      .split(" ")
      .filter((part) => part.trim() !== "")
      .map((keyValue) => {
        return {
          key: keyValue.split(":")[0],
          value: keyValue.split(":")[1],
        };
      })
      .every(({ key, value }) => {
        if (!requiredFields.find((field) => field.key === key)) return true;

        return requiredFields
          .find((field) => field.key === key)
          .condition(value);
      });
  }).length;

console.log("4a", A);
console.log("4b", B);
