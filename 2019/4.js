const validate = number => {
  const numberArr = (number + "").split("").map(num => parseInt(num, 10));

  const hasDoubleNumbers =
    numberArr.filter(num => (number + "").indexOf(num + "" + num) !== -1)
      .length > 0;
  const isIncreasing =
    numberArr
      .map((num, index, arr) => {
        return index == 0 || num >= arr[index - 1];
      })
      .indexOf("0") === -1;

  return hasDoubleNumbers && isIncreasing;
};

const parseRange = rangeString =>
  rangeString.split("-").map(x => parseInt(x, 10));

let numberOfValid = 0;

const [lowest, highest] = parseRange("153517-630395");

for (let i = lowest; i <= highest; i++) {
  if (validate(i)) numberOfValid++;
}

console.log("4a", numberOfValid);
