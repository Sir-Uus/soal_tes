const readline = require("readline");

const kamus = {
  A: 0,B: 1,C: 1,D: 1,E: 2,F: 3,G: 3,H: 3,I: 4,J: 5,
  K: 5,L: 5,M: 5,N: 5,O: 6,P: 7,Q: 7,R: 7,S: 7,T: 7,
  U: 8,V: 9,W: 9,X: 9,Y: 9,Z: 9,a: 9,b: 8,c: 8,d: 8,
  e: 7,f: 6,g: 6,h: 6,i: 5,j: 4,k: 4,l: 4,m: 4,n: 4,
  o: 3,p: 2,q: 2,r: 2,s: 2,t: 2,u: 2,v: 0,w: 0,x: 0,
  y: 0,z: 0," ": 0,
};

function angkaKeHuruf(numbers) {
  const reverseKamus = Object.entries(kamus).reduce((acc, [char, value]) => {
    if (!acc[value]) acc[value] = char;
    return acc;
  }, {});

  return numbers.map((num) => reverseKamus[num]).join(" ");
}

function uraiKeAngka(target) {
  const numbers = [];
  let currentSum = 0;
  let nextNumber = 0;

  while (currentSum < target) {
    if (currentSum + nextNumber > target) {
      nextNumber = 0;
    }
    numbers.push(nextNumber);
    currentSum += nextNumber;
    nextNumber++;
  }
  return numbers;
}

function konversi(sentence) {
  return Array.from(sentence).reduce((result, char) => {
    return kamus.hasOwnProperty(char) ? result + kamus[char] : result;
  }, "");
}

function operasiBergantian(sentence) {
  const values = Array.from(sentence).map((char) => (kamus.hasOwnProperty(char) ? kamus[char] : 0));

  let result = values[0] || 0;
  for (let i = 1; i < values.length; i++) {
    if (i % 2 === 0) {
      result -= values[i];
    } else {
      result += values[i];
    }
  }
  return result;
}

function operasiTransformasiAngka(values) {
  const newValues = [...values];
  if (newValues.length >= 2) {
    newValues[newValues.length - 2] = 1;
    newValues[newValues.length - 1] = 2;
  }
  return newValues;
}

function cekDanUbahGenap(huruf) {
  const values = Array.from(huruf).map((char) => (kamus.hasOwnProperty(char) ? kamus[char] : null));
  const hasilUbah = values.map((value) => (value !== null && value % 2 === 0 ? value + 1 : value));

  return hasilUbah;
}

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

rl.question("Masukkan kalimat: ", (input) => {
  const hasilKonversi = konversi(input);
  let hasilOperasi = operasiBergantian(input);
  const plushasilOperasi = Math.abs(hasilOperasi);
  const angkaIndividu = uraiKeAngka(plushasilOperasi);
  const hasilHuruf = angkaKeHuruf(angkaIndividu);
  const hasilTransformasiAngka = operasiTransformasiAngka(uraiKeAngka(plushasilOperasi));
  const hasilLog4 = angkaKeHuruf(hasilTransformasiAngka);
  const hasilLog5 = cekDanUbahGenap(hasilLog4.split(" "));

  console.log(`1. Hasil no 1: ${hasilKonversi}`);
  console.log(`2. Hasil no 2: ${hasilOperasi}`);
  console.log(`3. Hasil no 3: ${hasilHuruf}`);
  console.log(`4. Hasil no 4: ${hasilLog4}`);
  console.log(`5. Hasil no 5: ${hasilLog5.join(" ")}`);

  rl.close();
});
