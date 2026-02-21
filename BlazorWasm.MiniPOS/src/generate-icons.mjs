import fs from "node:fs/promises";
import path from "node:path";
import { fileURLToPath } from "node:url";

import { Resvg } from "@resvg/resvg-js";
import toIco from "to-ico";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

// Source: data-URI SVG in wwwroot/index.html (favicon link)
// Updated to use primary orange (#f97316) and transparent background.
const svg = `
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
  <path d="M6 2L3 6v14a2 2 0 002 2h14a2 2 0 002-2V6l-3-4z"/>
  <line x1="3" y1="6" x2="21" y2="6"/>
  <path d="M16 10a4 4 0 01-8 0"/>
</svg>
`.trim();

function renderPngBuffer(sizePx) {
  const resvg = new Resvg(svg, {
    fitTo: { mode: "width", value: sizePx },
    font: { loadSystemFonts: false },
  });
  return resvg.render().asPng();
}

async function main() {
  const root = path.resolve(__dirname, "..");
  const wwwroot = path.join(root, "wwwroot");

  const logoPath = path.join(wwwroot, "logo.png");
  const faviconPath = path.join(wwwroot, "favicon.ico");

  const logoPng = renderPngBuffer(512);
  await fs.writeFile(logoPath, logoPng);

  const ico = await toIco([renderPngBuffer(16), renderPngBuffer(32), renderPngBuffer(48)]);
  await fs.writeFile(faviconPath, ico);

  // eslint-disable-next-line no-console
  console.log(`Wrote ${path.relative(root, logoPath)} and ${path.relative(root, faviconPath)}`);
}

main().catch((err) => {
  // eslint-disable-next-line no-console
  console.error(err);
  process.exit(1);
});

