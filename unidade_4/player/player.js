const { createAudio } = require('node-mp3-player')
const Audio = createAudio();

process.argv.forEach(async arg => {
  if (arg.includes('--music')) {
    const [, path] = arg.split('=');
    const music = await Audio(path);
    await music.play();
    await music.stop();
  }
});