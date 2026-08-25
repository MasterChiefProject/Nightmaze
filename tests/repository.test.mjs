import test from 'node:test';
import assert from 'node:assert/strict';
import { existsSync, readFileSync } from 'node:fs';
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';

const here = dirname(fileURLToPath(import.meta.url));
const root = join(here, '..');
const read = path => readFileSync(join(root, path), 'utf8');

test('Unity project version is pinned to Unity 6.0.47f1', () => {
  assert.match(read('ProjectSettings/ProjectVersion.txt'), /m_EditorVersion:\s*6000\.0\.47f1/);
});

test('production scene list contains only Nightmaze scenes', () => {
  const buildSettings = read('ProjectSettings/EditorBuildSettings.asset');
  assert.doesNotMatch(buildSettings, /SampleScene/);
  for (const scene of ['MainMenuScene', 'TryAgainScene', 'WinScene', 'GameScene']) {
    assert.match(buildSettings, new RegExp(`Assets/Scenes/${scene}\\.unity`));
  }
});

test('deployed WebGL build files are committed', () => {
  for (const file of [
    'docs/Build/docs.loader.js',
    'docs/Build/docs.data.unityweb',
    'docs/Build/docs.framework.js.unityweb',
    'docs/Build/docs.wasm.unityweb',
    'docs/.nojekyll',
  ]) {
    assert.equal(existsSync(join(root, file)), true, `${file} must exist`);
  }
});

test('deployed browser shell defaults to dark mode and exposes a theme switch', () => {
  const html = read('docs/index.html');
  assert.match(html, /<html lang="en" data-theme="dark">/);
  assert.match(html, /id="theme-toggle"/);
  assert.match(html, /content="dark light"/);
  assert.match(html, /TemplateData\/shell\.js/);
  assert.match(html, /MasterChiefProject\/Nightmaze/);
});

test('theme preference is persistent and falls back to dark mode', () => {
  const shell = read('docs/TemplateData/shell.js');
  assert.match(shell, /nightmaze-web-theme-v1/);
  assert.match(shell, /=== 'light' \? 'light' : 'dark'/);
  assert.match(shell, /localStorage\.setItem/);
});

test('custom Unity WebGL template contains required build macros', () => {
  const template = read('Assets/WebGLTemplates/Nightmaze/index.html');
  for (const macro of ['LOADER_FILENAME', 'DATA_FILENAME', 'FRAMEWORK_FILENAME', 'CODE_FILENAME']) {
    assert.match(template, new RegExp(`\\{\\{\\{ ${macro} \\}\\}\\}`));
  }
  assert.match(template, /id="theme-toggle"/);
});

test('death audio path is reachable and death trigger is single-shot', () => {
  const death = read('Assets/Scripts/Death.cs');
  assert.match(death, /private bool triggered;/);
  assert.match(death, /audioSource\.Play\(\)/);
  assert.doesNotMatch(death, /private float PlayRandomDeathSound\(\)\s*\{\s*return 0f;\s*if/s);
});
