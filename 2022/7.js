const fs = require("fs");

const { tree } = fs
  .readFileSync("7.txt")
  .toString()
  .split("\r\n")
  .reduce(
    (prev, curr) => {
      const currentArr = curr.split(" ");
      if (curr.indexOf("$ cd") === 0) {
        if (currentArr[2] === "..") {
          prev.currentPath = prev.currentPath
            .split("/")
            .slice(0, prev.currentPath.split("/").length - 1)
            .join("/");
        } else {
          const newPath = `${prev.currentPath}/${currentArr[2]}`.replace(
            "//",
            "/"
          );
          prev.currentPath = newPath;
          prev.tree[newPath] = 0;
        }
      } else if (!isNaN(parseInt(currentArr[0]))) {
        prev.tree[prev.currentPath] += parseInt(currentArr[0]);
      }

      return prev;
    },
    { currentPath: "", tree: {} }
  );

const sumTreeItems = Object.entries(tree).reduce(
  (prev, [path, count], _, arr) => {
    const children = arr.filter(
      (fullTree) => fullTree[0].indexOf(path) === 0 && fullTree[0] !== path
    );

    const sum = children.reduce((p, c) => p + c[1], count);

    prev.tree[path] = sum;

    if (sum < 100000) {
      prev.countSmall += sum;
    }

    return prev;
  },
  { countSmall: 0, tree: {} }
);

console.log("a", sumTreeItems.countSmall); //1581595

const total = 70000000;
const totalNeeded = 30000000;
const totalSize = sumTreeItems.tree["/"];
const totalAvailable = total - totalSize;
const toFind = Math.abs(totalAvailable - totalNeeded);

const closest = Object.entries(sumTreeItems.tree)
  .sort((a, b) => a[1] - b[1])
  .filter(([_, val]) => {
    return val >= toFind;
  })[0];

console.log("b", closest);
