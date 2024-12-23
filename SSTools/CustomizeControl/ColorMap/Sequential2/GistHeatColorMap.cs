﻿using System.Drawing;
namespace SSTools.ColorMap.Sequential2
{
	/// <summary>
	/// GistHeatカラーマップ
	/// </summary>
	public class GistHeatColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GistHeatColorMap()
		{
			colorMap = gist_heat_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] gist_heat_map_ =
		{
			Color.FromArgb(0,0,0),		// 0
			Color.FromArgb(1,0,0),		// 1
			Color.FromArgb(3,0,0),		// 2
			Color.FromArgb(4,0,0),		// 3
			Color.FromArgb(6,0,0),		// 4
			Color.FromArgb(7,0,0),		// 5
			Color.FromArgb(9,0,0),		// 6
			Color.FromArgb(10,0,0),		// 7
			Color.FromArgb(12,0,0),		// 8
			Color.FromArgb(13,0,0),		// 9
			Color.FromArgb(15,0,0),		// 10
			Color.FromArgb(16,0,0),		// 11
			Color.FromArgb(18,0,0),		// 12
			Color.FromArgb(19,0,0),		// 13
			Color.FromArgb(21,0,0),		// 14
			Color.FromArgb(22,0,0),		// 15
			Color.FromArgb(24,0,0),		// 16
			Color.FromArgb(25,0,0),		// 17
			Color.FromArgb(27,0,0),		// 18
			Color.FromArgb(28,0,0),		// 19
			Color.FromArgb(30,0,0),		// 20
			Color.FromArgb(31,0,0),		// 21
			Color.FromArgb(32,0,0),		// 22
			Color.FromArgb(34,0,0),		// 23
			Color.FromArgb(36,0,0),		// 24
			Color.FromArgb(37,0,0),		// 25
			Color.FromArgb(39,0,0),		// 26
			Color.FromArgb(40,0,0),		// 27
			Color.FromArgb(42,0,0),		// 28
			Color.FromArgb(43,0,0),		// 29
			Color.FromArgb(44,0,0),		// 30
			Color.FromArgb(46,0,0),		// 31
			Color.FromArgb(48,0,0),		// 32
			Color.FromArgb(49,0,0),		// 33
			Color.FromArgb(51,0,0),		// 34
			Color.FromArgb(52,0,0),		// 35
			Color.FromArgb(54,0,0),		// 36
			Color.FromArgb(55,0,0),		// 37
			Color.FromArgb(56,0,0),		// 38
			Color.FromArgb(58,0,0),		// 39
			Color.FromArgb(60,0,0),		// 40
			Color.FromArgb(61,0,0),		// 41
			Color.FromArgb(63,0,0),		// 42
			Color.FromArgb(64,0,0),		// 43
			Color.FromArgb(65,0,0),		// 44
			Color.FromArgb(67,0,0),		// 45
			Color.FromArgb(69,0,0),		// 46
			Color.FromArgb(70,0,0),		// 47
			Color.FromArgb(72,0,0),		// 48
			Color.FromArgb(73,0,0),		// 49
			Color.FromArgb(75,0,0),		// 50
			Color.FromArgb(76,0,0),		// 51
			Color.FromArgb(78,0,0),		// 52
			Color.FromArgb(79,0,0),		// 53
			Color.FromArgb(81,0,0),		// 54
			Color.FromArgb(82,0,0),		// 55
			Color.FromArgb(84,0,0),		// 56
			Color.FromArgb(85,0,0),		// 57
			Color.FromArgb(87,0,0),		// 58
			Color.FromArgb(88,0,0),		// 59
			Color.FromArgb(89,0,0),		// 60
			Color.FromArgb(91,0,0),		// 61
			Color.FromArgb(93,0,0),		// 62
			Color.FromArgb(94,0,0),		// 63
			Color.FromArgb(96,0,0),		// 64
			Color.FromArgb(97,0,0),		// 65
			Color.FromArgb(98,0,0),		// 66
			Color.FromArgb(100,0,0),		// 67
			Color.FromArgb(102,0,0),		// 68
			Color.FromArgb(103,0,0),		// 69
			Color.FromArgb(105,0,0),		// 70
			Color.FromArgb(106,0,0),		// 71
			Color.FromArgb(108,0,0),		// 72
			Color.FromArgb(109,0,0),		// 73
			Color.FromArgb(110,0,0),		// 74
			Color.FromArgb(112,0,0),		// 75
			Color.FromArgb(113,0,0),		// 76
			Color.FromArgb(115,0,0),		// 77
			Color.FromArgb(117,0,0),		// 78
			Color.FromArgb(118,0,0),		// 79
			Color.FromArgb(120,0,0),		// 80
			Color.FromArgb(121,0,0),		// 81
			Color.FromArgb(122,0,0),		// 82
			Color.FromArgb(124,0,0),		// 83
			Color.FromArgb(126,0,0),		// 84
			Color.FromArgb(127,0,0),		// 85
			Color.FromArgb(129,0,0),		// 86
			Color.FromArgb(130,0,0),		// 87
			Color.FromArgb(131,0,0),		// 88
			Color.FromArgb(133,0,0),		// 89
			Color.FromArgb(134,0,0),		// 90
			Color.FromArgb(136,0,0),		// 91
			Color.FromArgb(138,0,0),		// 92
			Color.FromArgb(139,0,0),		// 93
			Color.FromArgb(141,0,0),		// 94
			Color.FromArgb(142,0,0),		// 95
			Color.FromArgb(144,0,0),		// 96
			Color.FromArgb(145,0,0),		// 97
			Color.FromArgb(147,0,0),		// 98
			Color.FromArgb(148,0,0),		// 99
			Color.FromArgb(150,0,0),		// 100
			Color.FromArgb(151,0,0),		// 101
			Color.FromArgb(153,0,0),		// 102
			Color.FromArgb(154,0,0),		// 103
			Color.FromArgb(156,0,0),		// 104
			Color.FromArgb(157,0,0),		// 105
			Color.FromArgb(159,0,0),		// 106
			Color.FromArgb(160,0,0),		// 107
			Color.FromArgb(162,0,0),		// 108
			Color.FromArgb(163,0,0),		// 109
			Color.FromArgb(165,0,0),		// 110
			Color.FromArgb(166,0,0),		// 111
			Color.FromArgb(168,0,0),		// 112
			Color.FromArgb(169,0,0),		// 113
			Color.FromArgb(171,0,0),		// 114
			Color.FromArgb(172,0,0),		// 115
			Color.FromArgb(174,0,0),		// 116
			Color.FromArgb(175,0,0),		// 117
			Color.FromArgb(177,0,0),		// 118
			Color.FromArgb(178,0,0),		// 119
			Color.FromArgb(179,0,0),		// 120
			Color.FromArgb(181,0,0),		// 121
			Color.FromArgb(182,0,0),		// 122
			Color.FromArgb(184,0,0),		// 123
			Color.FromArgb(186,0,0),		// 124
			Color.FromArgb(187,0,0),		// 125
			Color.FromArgb(189,0,0),		// 126
			Color.FromArgb(190,0,0),		// 127
			Color.FromArgb(192,0,0),		// 128
			Color.FromArgb(193,2,0),		// 129
			Color.FromArgb(195,4,0),		// 130
			Color.FromArgb(196,6,0),		// 131
			Color.FromArgb(197,8,0),		// 132
			Color.FromArgb(199,11,0),		// 133
			Color.FromArgb(201,13,0),		// 134
			Color.FromArgb(202,15,0),		// 135
			Color.FromArgb(204,16,0),		// 136
			Color.FromArgb(205,18,0),		// 137
			Color.FromArgb(207,20,0),		// 138
			Color.FromArgb(208,22,0),		// 139
			Color.FromArgb(210,25,0),		// 140
			Color.FromArgb(211,27,0),		// 141
			Color.FromArgb(213,29,0),		// 142
			Color.FromArgb(214,31,0),		// 143
			Color.FromArgb(216,32,0),		// 144
			Color.FromArgb(217,34,0),		// 145
			Color.FromArgb(219,36,0),		// 146
			Color.FromArgb(220,38,0),		// 147
			Color.FromArgb(221,40,0),		// 148
			Color.FromArgb(223,43,0),		// 149
			Color.FromArgb(225,45,0),		// 150
			Color.FromArgb(226,47,0),		// 151
			Color.FromArgb(227,48,0),		// 152
			Color.FromArgb(229,50,0),		// 153
			Color.FromArgb(230,52,0),		// 154
			Color.FromArgb(232,54,0),		// 155
			Color.FromArgb(234,57,0),		// 156
			Color.FromArgb(235,59,0),		// 157
			Color.FromArgb(237,61,0),		// 158
			Color.FromArgb(238,63,0),		// 159
			Color.FromArgb(240,65,0),		// 160
			Color.FromArgb(241,66,0),		// 161
			Color.FromArgb(243,68,0),		// 162
			Color.FromArgb(244,70,0),		// 163
			Color.FromArgb(245,72,0),		// 164
			Color.FromArgb(247,75,0),		// 165
			Color.FromArgb(249,77,0),		// 166
			Color.FromArgb(250,79,0),		// 167
			Color.FromArgb(252,81,0),		// 168
			Color.FromArgb(253,82,0),		// 169
			Color.FromArgb(255,84,0),		// 170
			Color.FromArgb(255,86,0),		// 171
			Color.FromArgb(255,89,0),		// 172
			Color.FromArgb(255,91,0),		// 173
			Color.FromArgb(255,93,0),		// 174
			Color.FromArgb(255,95,0),		// 175
			Color.FromArgb(255,97,0),		// 176
			Color.FromArgb(255,98,0),		// 177
			Color.FromArgb(255,100,0),		// 178
			Color.FromArgb(255,102,0),		// 179
			Color.FromArgb(255,104,0),		// 180
			Color.FromArgb(255,107,0),		// 181
			Color.FromArgb(255,109,0),		// 182
			Color.FromArgb(255,111,0),		// 183
			Color.FromArgb(255,113,0),		// 184
			Color.FromArgb(255,114,0),		// 185
			Color.FromArgb(255,116,0),		// 186
			Color.FromArgb(255,118,0),		// 187
			Color.FromArgb(255,121,0),		// 188
			Color.FromArgb(255,123,0),		// 189
			Color.FromArgb(255,125,0),		// 190
			Color.FromArgb(255,127,0),		// 191
			Color.FromArgb(255,129,2),		// 192
			Color.FromArgb(255,131,6),		// 193
			Color.FromArgb(255,132,10),		// 194
			Color.FromArgb(255,134,14),		// 195
			Color.FromArgb(255,136,18),		// 196
			Color.FromArgb(255,139,23),		// 197
			Color.FromArgb(255,141,27),		// 198
			Color.FromArgb(255,143,31),		// 199
			Color.FromArgb(255,145,34),		// 200
			Color.FromArgb(255,147,38),		// 201
			Color.FromArgb(255,148,42),		// 202
			Color.FromArgb(255,150,46),		// 203
			Color.FromArgb(255,153,51),		// 204
			Color.FromArgb(255,155,55),		// 205
			Color.FromArgb(255,157,59),		// 206
			Color.FromArgb(255,159,63),		// 207
			Color.FromArgb(255,161,66),		// 208
			Color.FromArgb(255,163,70),		// 209
			Color.FromArgb(255,164,74),		// 210
			Color.FromArgb(255,166,78),		// 211
			Color.FromArgb(255,168,82),		// 212
			Color.FromArgb(255,171,87),		// 213
			Color.FromArgb(255,173,91),		// 214
			Color.FromArgb(255,175,95),		// 215
			Color.FromArgb(255,177,98),		// 216
			Color.FromArgb(255,179,102),		// 217
			Color.FromArgb(255,180,106),		// 218
			Color.FromArgb(255,182,110),		// 219
			Color.FromArgb(255,185,115),		// 220
			Color.FromArgb(255,187,119),		// 221
			Color.FromArgb(255,189,123),		// 222
			Color.FromArgb(255,191,127),		// 223
			Color.FromArgb(255,193,131),		// 224
			Color.FromArgb(255,195,134),		// 225
			Color.FromArgb(255,196,138),		// 226
			Color.FromArgb(255,198,142),		// 227
			Color.FromArgb(255,200,146),		// 228
			Color.FromArgb(255,203,151),		// 229
			Color.FromArgb(255,205,155),		// 230
			Color.FromArgb(255,207,159),		// 231
			Color.FromArgb(255,209,163),		// 232
			Color.FromArgb(255,211,166),		// 233
			Color.FromArgb(255,212,170),		// 234
			Color.FromArgb(255,214,174),		// 235
			Color.FromArgb(255,217,179),		// 236
			Color.FromArgb(255,219,183),		// 237
			Color.FromArgb(255,221,187),		// 238
			Color.FromArgb(255,223,191),		// 239
			Color.FromArgb(255,225,195),		// 240
			Color.FromArgb(255,227,198),		// 241
			Color.FromArgb(255,228,202),		// 242
			Color.FromArgb(255,230,206),		// 243
			Color.FromArgb(255,232,210),		// 244
			Color.FromArgb(255,235,215),		// 245
			Color.FromArgb(255,237,219),		// 246
			Color.FromArgb(255,239,223),		// 247
			Color.FromArgb(255,241,227),		// 248
			Color.FromArgb(255,243,230),		// 249
			Color.FromArgb(255,244,234),		// 250
			Color.FromArgb(255,246,238),		// 251
			Color.FromArgb(255,249,243),		// 252
			Color.FromArgb(255,251,247),		// 253
			Color.FromArgb(255,253,251),		// 254
			Color.FromArgb(255,255,255),		// 255
		};
	}
}
